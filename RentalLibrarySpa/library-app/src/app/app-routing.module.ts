import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { OverviewComponent } from './overview/overview.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoginComponent } from './login/login.component';
import { RegisterMemberComponent } from './register-member/register-member.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './helpers/auth.guard';
import { RoleGuard } from './helpers/role.guard';
import { AddBindingComponent } from './add-binding/add-binding.component';
import { AddCategoryComponent } from './add-category/add-category.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterMemberComponent},
  {path:'', component:LandingPageComponent,canActivate:[AuthGuard],
   children:[
    {path:'overview',component:OverviewComponent,data:{role:'User'},canActivate:[RoleGuard]},
    {path:'add-binding',component:AddBindingComponent,data:{role:'Administrator'},canActivate:[RoleGuard]},
    {path:'add-category',component:AddCategoryComponent,data:{role:'Administrator'},canActivate:[RoleGuard]}
   ] 
  },
  { path:'404',component:NotFoundComponent},
  { path: '**', redirectTo: '404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
