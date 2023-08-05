import { Component } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(authService:AuthenticationService,router:Router){
       if(authService.userValue)
       {
         router.navigate(['overview']);
       }
  }
}
