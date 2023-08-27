import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent {

  username:string="";

  constructor(public authService:AuthenticationService,router:Router)
  {
    if(!authService.userValue)
    {
      router.navigate(['login']);
    }
/*     else{
      this.username = authService.userValue.username;
    } */

  }

  signOut()
  {
    this.authService.logout();
  }
}
