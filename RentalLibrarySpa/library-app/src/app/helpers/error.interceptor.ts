import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService, private toastr: ToastrService,) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if ([401, 403].includes(err.status)) {
                // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                if (err.status == 401) {
                    this.toastr.success("Unauthorized", "401");
                }
                else {
                    this.toastr.success("Forbidden", "403");
                }

                this.authenticationService.logout();
            }

            const error = err.error?.message || err.statusText;

            if ([400, 404, 500].includes(err.status)) {
                if (err.status == 400)
                    this.toastr.error("Sorry! Could not be done.");
                else if (err.status == 404)
                    this.toastr.error("Not Found!");
                else if (err.status == 500)
                    this.toastr.error("Internal Server Error!");
            }


            return throwError(() => error);
        }))
    }
}