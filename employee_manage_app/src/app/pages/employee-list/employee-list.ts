import { Component, inject, OnInit, signal } from '@angular/core';
import { IEmployeeListModel } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee-service';
import { RouterLink } from '@angular/router';

@Component({
  standalone: true,
  imports: [RouterLink],
  selector: 'app-employee-list',
  styleUrl: './employee-list.css',
  templateUrl: './employee-list.html',
})
export class EmployeeList implements OnInit {

  employeeList = signal<IEmployeeListModel[]>([]);
  empserv = inject(EmployeeService)

  ngOnInit(): void {
    console.log(this.getAllEmp());
  }

  getAllEmp(){
    this.empserv.getAllEmployees().subscribe({
      next: (res: IEmployeeListModel[])=>{
        this.employeeList.set(res)
      }
    })
  }
}
