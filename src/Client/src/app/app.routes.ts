import { inject } from '@angular/core';
import { CanActivateFn, Router, Routes } from '@angular/router';
import { AuthService } from './services/auth.service';

const canActivateVerified: CanActivateFn = () => {
  if (inject(AuthService).isVerified()) {
    return true;
  } else {
    inject(Router).navigate(['/']);
  }

  return false;
};

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./home/home.component').then(mod => mod.HomeComponent),
    data: { animation: 'HomePage' },
  },
  {
    path: 'books',
    loadComponent: () =>
      import('./books-page/books-page.component').then(
        mod => mod.BooksPageComponent
      ),
    canActivate: [canActivateVerified],
    data: { animation: 'BooksPage' },
  },
  {
    path: '**',
    redirectTo: '',
  },
];
