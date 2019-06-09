import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {HttpClient} from '@angular/common/http';
import { TeamMates } from '../models/TeamMates';
import { TeamManagment } from '../models/TeamManagment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

  getUsersWithRoles() {

    return this.http.get(this.baseUrl + 'admin/usersWithRoles');
  }
  getUsersByName(name: string) {

    return this.http.get(this.baseUrl + 'admin/userslogin/' + name);
  }
  getRoles() {
    return this.http.get(this.baseUrl + 'admin/roles');
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'admin/user/' + id);
  }
  deleteTeam(id: number) {
    return this.http.delete(this.baseUrl + 'admin/team/' + id);
  }
  getTeam() {
    return this.http.get(this.baseUrl + 'admin/teams');
  }

  getAllTeamManagment() {
    return this.http.get(this.baseUrl + 'admin/teamsmanagment');
  }

  addTeamMates(formData: TeamMates) {
    return this.http.post(this.baseUrl + 'admin/userteam', formData);
  }

  addTeam(form: TeamManagment) {
    return this.http.post(this.baseUrl + 'admin/teams', form);
  }
}
