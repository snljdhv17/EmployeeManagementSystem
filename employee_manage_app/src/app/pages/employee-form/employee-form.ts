import { Component, inject } from '@angular/core';
import { EmployeeModel } from '../../models/employee.model';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../services/employee-service';
import { async, Observable } from 'rxjs';
import { DesignationListModel } from '../../models/department.model';
import { Master } from '../../services/master';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  standalone: true,
  imports: [FormsModule, AsyncPipe],
  selector: 'app-employee-form',
  styleUrl: './employee-form.css',
  templateUrl: './employee-form.html',
})
export class EmployeeForm {

  empSer = inject(EmployeeService);
  masterServ = inject(Master);
  activeRoute = inject(ActivatedRoute);

  newEmployeeObj: EmployeeModel = new EmployeeModel();

  loggedEmpData : EmployeeModel= new EmployeeModel();



  $designationList: Observable<DesignationListModel[]> = new Observable <DesignationListModel[]>();
 
  constructor(){
    const localData = localStorage.getItem('emploginUser');
    if(localData != null){
      this.loggedEmpData = JSON.parse(localData)
    }
    this.activeRoute.params.subscribe((res:any)=>{
      if(res.id != 0){
        this.newEmployeeObj.employeeId = res.id;
        this.getEmpById();
      }
    })
     this.$designationList = this.masterServ.getAllDesignations();
  }

  getEmpById(){
    this.empSer.getEmpById(this.newEmployeeObj.employeeId).subscribe({
      next:(result:any)=>{
        this.newEmployeeObj = result;
      }
    })
  }
  onSaveEmp(){
    this.empSer.saveEmployee(this.newEmployeeObj).subscribe({
      next:(result)=>{
      this.newEmployeeObj = new EmployeeModel();
      },error:(err)=>{

      }
    })
  }

}
