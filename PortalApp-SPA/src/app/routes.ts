import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { TeamComponent } from './team/team.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './guards/auth.guard';


export const appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'main', component: MainComponent},
      {path: 'team', component: TeamComponent},
    ]
  },
  {path: '**', redirectTo: '', pathMatch: 'full'},
];
