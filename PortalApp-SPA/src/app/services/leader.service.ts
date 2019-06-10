import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Opinion } from '../models/Opinion';

@Injectable({
  providedIn: 'root'
})
export class LeaderService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

    getUsersWithRoles(leaderid: number) {
      return this.http.get(this.baseUrl + 'leader/leaderteam/' + leaderid);
    }
    AddOpinion(formData: Opinion) {
      return this.http.post(this.baseUrl + 'leader/opinion', formData);
    }
    GetUserOpinion(UserId: number) {
      return this.http.get(this.baseUrl + 'leader/useropinions/' + UserId);
    }
    GetLeader(LeaderId: number) {
      return this.http.get(this.baseUrl + 'leader/' + LeaderId);
    }
}
