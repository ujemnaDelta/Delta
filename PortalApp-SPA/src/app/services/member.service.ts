import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

    GetUserOpinion(UserId: number) {
      return this.http.get(this.baseUrl + 'user/useropinions/' + UserId);
    }
    GetUser(UserId: number) {
      return this.http.get(this.baseUrl + 'user/' + UserId);
    }
    getTeamMates(userid: number) {
      return this.http.get(this.baseUrl + 'user/userteam/' + userid);
    }
}
