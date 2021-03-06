import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { TeamComponent } from './team/team.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import {HrPanelComponent} from './hr/hr-panel/hr-panel.component';
import { LeaderPanelComponent } from './leader/leader-panel/leader-panel.component';


export const appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'main', component: MainComponent, data: {roles: ['Member']}},
      {path: 'team', component: TeamComponent, data: {roles: ['Member']}},
      {path: 'adminPanel', component: AdminPanelComponent, data: {roles: ['Admin']}},
      {path: 'hrPanel', component: HrPanelComponent , data: { roles: ['HR'] }},
      {path: 'leaderPanel', component: LeaderPanelComponent , data: { roles: ['Leader'] }},
    ]
  },
  {path: '**', redirectTo: '', pathMatch: 'full'},
];
