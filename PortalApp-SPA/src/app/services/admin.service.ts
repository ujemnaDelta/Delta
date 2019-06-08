import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  getUsersWithRoles() {

    return this.http.get(this.baseUrl + 'admin/usersWithRoles');
  }

  getRoles() {
    return this.http.get(this.baseUrl + 'admin/roles');
  }

  deleteUser(id: string) {
    return this.http.get(this.baseUrl + 'admin/user/' + id);
  }

  getTeam() {
    return this.http.get(this.baseUrl + 'admin/teams');
  }

  getAllTeamManagment() {
    return this.http.get(this.baseUrl + 'admin/teamsmanagment');
  }
}
