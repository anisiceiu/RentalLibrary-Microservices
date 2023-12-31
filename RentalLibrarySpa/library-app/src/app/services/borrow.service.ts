import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReserveRequest } from '../models/request';

@Injectable({
  providedIn: 'root'
})
export class BorrowService {

  private baseUrl = 'https://localhost:7225'; // Replace with your API base URL

  constructor(private http: HttpClient) {}
  
  public addDays(date:Date, days:number):Date {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }

  public reseveBook(request:ReserveRequest)
  {
    return this.http.post<any>(`${this.baseUrl}/Borrow/BookReserveRequest`,request);
  }

  public issueBook(request:ReserveRequest)
  {
    return this.http.post<any>(`${this.baseUrl}/Borrow/BookIssueRequest`,request);
  }
  
  public bookIssueRequestRejected(request:ReserveRequest)
  {
    return this.http.post<any>(`${this.baseUrl}/Borrow/BookIssueRequestRejected`,request);
  }

  public bookRenewRequest(request:ReserveRequest)
  {
    return this.http.post<any>(`${this.baseUrl}/Borrow/BookRenewRequest`,request);
  }

  public getBookRequests()
  {
    return this.http.get<any>(`${this.baseUrl}/Borrow/GetBookRequests`);
  }

  public getMyBorrowedBook(id:number=0)
  {
    return this.http.get<any>(`${this.baseUrl}/Borrow/GetBorrowedBookByMemberId/${id}`);
  }

  public getBorrowedBookByMemberNo(memberNo:string)
  {
    return this.http.get<any>(`${this.baseUrl}/Borrow/GetBorrowedBookByMemberNo/${memberNo}`);
  }
  public GetAllBorrowedBooks()
  {
    return this.http.get<any>(`${this.baseUrl}/Borrow/GetAllBorrowedBooks`);
  }
}
