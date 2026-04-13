import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './features/home/home';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Rooms } from './features/rooms/rooms';
import { RoomDetails } from './features/room-details/room-details';
import { Payment } from './features/payment/payment';
import { Dashboard } from './features/dashboard/dashboard';
import { AuthGuard } from './core/gurds/auth.guard';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'login', component: Login},
  { path: 'register', component: Register},
  { path: 'rooms', component: Rooms},
  { path: 'rooms/:id', component: RoomDetails },
  { path: 'payment/:id', component: Payment, canActivate: [AuthGuard] },
  { path: 'dashboard', component: Dashboard, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
