import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {
  constructor(private authService:AuthenticationService,router:Router)
  {
    if(!authService.userValue)
    {
      router.navigate(['login']);
    }

  }

  signOut()
  {
    this.authService.logout();
  }
}
