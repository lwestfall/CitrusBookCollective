import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../app-state';
import { MeetingsModule } from '../meetings/meetings.module';
import { LoginComponent } from '../navbar/login/login.component';
import { selectAuthenticatedUser } from '../users/state/users.selectors';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [CommonModule, LoginComponent, RouterModule, MeetingsModule],
  standalone: true,
})
export class HomeComponent {
  signedIn = false;
  verified = false;

  constructor(store: Store<AppState>) {
    const obs = store.select(selectAuthenticatedUser);

    obs.subscribe(user => {
      if (user) {
        this.signedIn = true;
        this.verified = user.roles?.includes('Verified') ?? false;
      }
    });
  }
}
