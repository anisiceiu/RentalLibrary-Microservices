import { Component } from '@angular/core';
import { Member } from '../models/member';
import { MemberService } from '../services/member.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register-member',
  templateUrl: './register-member.component.html',
  styleUrls: ['./register-member.component.css']
})
export class RegisterMemberComponent {

  member: Member = new Member;

  constructor(private memberService:MemberService,private toastr:ToastrService)
  {

  }

  addMember()
  {
    if(this.member.password == this.member.reTypepassword)
    {
      this.memberService.addMember(this.member).subscribe((data)=>{
        if(data)
        {
          this.toastr.success('Member registered successfully',"Success");
        }
     },error=>{
        this.toastr.error("Could not add the member.")
     });
    }
    else{
      this.toastr.error('Password and retyped password does not match.',"Error");
    }

  }

}
