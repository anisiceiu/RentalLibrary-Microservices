import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  username: string='';
  password: string='';
  error: string='';

  constructor(private router: Router,private authService:AuthenticationService) {
    
   }

  ngOnInit(): void {
  }

  login()
  {
      this.authService.login(this.username,this.password).subscribe((data)=>{
        if(data && localStorage.getItem('user_token'))
        {
          console.log("here:",data);
            this.router.navigateByUrl('/overview');
        }
        else{
            this.error = 'Invalid username or password';
            console.log('login e esecilo...')
        }

      });
  }
}
