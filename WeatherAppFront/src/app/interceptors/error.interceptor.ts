import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {ToastrService} from "ngx-toastr";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error.status === 401) {
          localStorage.clear();
          location.reload();
        } else if (error.status === 400) {
          debugger;
          const mainErrorObject = error.error;
          const errors = mainErrorObject.errors;
          const title = mainErrorObject.title;

          if(!mainErrorObject.Success)

          if (errors) {
            for (const key in errors) {
              const value = errors[key];
              this.toastr.error(value, title);
            }
          } else {
            this.toastr.error(mainErrorObject.ExceptionMessage ?? 'An error occurred on the server');
          }

        } else if (error.status === 500) {
          this.toastr.error('An error occurred on the server');
        }
        return throwError(error);
      })
    );
  }
}
