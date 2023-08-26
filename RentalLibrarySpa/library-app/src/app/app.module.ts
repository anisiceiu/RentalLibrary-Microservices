import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { OverviewComponent } from './overview/overview.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import{BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { LoginComponent } from './login/login.component';
import { RegisterMemberComponent } from './register-member/register-member.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './helpers/auth.guard';
import { RoleGuard } from './helpers/role.guard';
import { AddCategoryComponent } from './add-category/add-category.component';
import { AddBindingComponent } from './add-binding/add-binding.component';
import { AddBookComponent } from './add-book/add-book.component';
import { ImgFallbackDirective } from './directives/img-fallback.directive';
import { RequestListComponent } from './request-list/request-list.component';
import { MyBorrowedBookComponent } from './my-borrowed-book/my-borrowed-book.component';

@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    OverviewComponent,
    NotFoundComponent,
    LoginComponent,
    RegisterMemberComponent,
    HomeComponent,
    AddCategoryComponent,
    AddBindingComponent,
    AddBookComponent,
    ImgFallbackDirective,
    RequestListComponent,
    MyBorrowedBookComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    PaginationModule.forRoot(),
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthGuard,
    RoleGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
