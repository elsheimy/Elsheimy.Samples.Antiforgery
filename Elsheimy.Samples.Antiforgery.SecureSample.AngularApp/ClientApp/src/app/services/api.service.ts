import { Inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable()
export class ApiService {
  constructor(
    @Inject("BASE_URL") private baseUrl: string,
    private http: HttpClient) {
  }

  balance(): Observable<number> {
    let url = `/api/balance`;
    return this.http.get<number>(url);
  }

  debit(amount: number): Observable<number> {
    // relative path
    let url = `/api/debit?amount=${amount}`;
    return this.request(url);
  }

  credit(amount: number): Observable<number> {
    // absolute path
    let url = this.baseUrl + `api/credit?amount=${amount}`;
    return this.request(url);
  }

  request(url: string): Observable<number> {
    let result: number;
    return this.http.post<number>(url, {});
  }
}
