import { inject } from '@angular/core';
import {
  CanActivateFn,
  Router,
  RouterStateSnapshot,
  Routes,
} from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { Observable, catchError, filter, of, switchMap, take, tap } from 'rxjs';
import { UserDto } from './api/models';
import { AppState } from './app-state';
import { BooksPageComponent } from './books/books-page/books-page.component';
import { MeetingsPageComponent } from './meetings/meetings-page/meetings-page.component';
import {
  getAuthenticatedUser,
  getAuthenticatedUserFailure,
  getAuthenticatedUserSuccess,
} from './users/state/users.actions';
import { selectAuthenticatedUser } from './users/state/users.selectors';
import { UsersPageComponent } from './users/users-page/users-page.component';

const canActivateVerified: CanActivateFn = (_, state): Observable<boolean> =>
  roleBasedCanActivate('Verified', state);

const canActivateAdmin: CanActivateFn = (_, state): Observable<boolean> =>
  roleBasedCanActivate('Admin', state);

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./home/home.component').then(mod => mod.HomeComponent),
    data: { animation: 'HomePage' },
  },
  {
    path: 'books',
    component: BooksPageComponent,
    canActivate: [canActivateVerified],
    data: { animation: 'BooksPage' },
  },
  {
    path: 'meetings',
    component: MeetingsPageComponent,
    canActivate: [canActivateVerified],
    data: { animation: 'MeetingsPage' },
  },
  {
    path: 'users',
    component: UsersPageComponent,
    canActivate: [canActivateAdmin],
    data: { animation: 'UsersPage' },
  },
  {
    path: '**',
    redirectTo: '',
  },
];

function roleBasedCanActivate(
  role: string,
  state: RouterStateSnapshot
): Observable<boolean> {
  const router = inject(Router);

  return getAuthenticatedUserLocal().pipe(
    switchMap(u => (u?.roles?.includes(role) ? of(true) : of(false))),
    catchError(() => of(false)),
    tap(isVerified => {
      if (!isVerified) {
        router.navigate(['/'], { queryParams: { returnUrl: state.url } });
      }
    })
  );
}

function getAuthenticatedUserLocal(): Observable<UserDto | null> {
  const store = inject(Store<AppState>);
  const actions$ = inject(Actions);

  return store.select(selectAuthenticatedUser).pipe(
    tap(user => {
      if (!user) {
        store.dispatch(getAuthenticatedUser());
      }
    }),
    switchMap(user => {
      if (user) {
        return of(user);
      } else {
        return actions$.pipe(
          ofType(getAuthenticatedUserSuccess, getAuthenticatedUserFailure),
          switchMap(() => store.select(selectAuthenticatedUser)),
          filter(user => user !== undefined),
          take(1)
        );
      }
    })
  );
}
