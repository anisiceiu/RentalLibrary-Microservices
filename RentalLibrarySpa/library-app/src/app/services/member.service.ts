import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  private baseUrl = 'https://localhost:7225'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  public addMember(member:Member)
  {
    return this.http.post<any>(`${this.baseUrl}/member/AddMember`,member);
  }
}
