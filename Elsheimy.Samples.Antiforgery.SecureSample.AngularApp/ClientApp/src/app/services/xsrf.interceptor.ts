import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpXsrfTokenExtractor } from "@angular/common/http";
import { HttpEvent, HttpHandler, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable()
export class XsrfInterceptor implements HttpInterceptor {
  constructor(private xsrfTokenExtractor: HttpXsrfTokenExtractor) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // load token
    let xsrfToken = this.xsrfTokenExtractor.getToken();

    if (xsrfToken != null) {
      // create a copy of the request and
      // append the XSRF token to the headers list
      const authorizedRequest = req.clone({
        withCredentials: true,
        headers: req.headers.set('X-XSRF-TOKEN', xsrfToken)
      });

      return next.handle(authorizedRequest);
    } else {
      return next.handle(req);
    }
  }
}
