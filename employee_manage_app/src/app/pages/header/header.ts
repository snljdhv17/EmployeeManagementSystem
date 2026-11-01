import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterLink, Router } from '@angular/router';
import { EmployeeModel } from '../../models/employee.model';

@Component({
  standalone: true, 
  imports: [CommonModule, RouterModule, RouterLink],
  selector: 'app-header',
  styleUrl: './header.css',
  templateUrl: './header.html',
})
export class Header {
   isCollapsed = false;
   route = inject(Router);
   loggedEmpData : EmployeeModel= new EmployeeModel();

   constructor(){
    const localData = localStorage.getItem('emploginUser');
    if(localData != null){
      this.loggedEmpData = JSON.parse(localData)
    }
   }

  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
  }

  onLogOff(){
     localStorage.removeItem("emploginUser");
     this.route.navigateByUrl("/login")

  }
}
