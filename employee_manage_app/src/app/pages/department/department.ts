import { Component , inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { DepartmentModel } from '../../models/department.model';
import { Master } from '../../services/master'

@Component({
  standalone: true, 
  imports: [FormsModule],
  selector: 'app-department',
  styleUrl: './department.css',
  templateUrl: './department.html',
})
export class Department implements OnInit{
  newDeptObj: DepartmentModel = new DepartmentModel();

  masterService = inject(Master)
  deptList= signal<DepartmentModel[]>([]);

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.getAllDepartments();
  }
  onSaveDept(){
   this.masterService.saveDept(this.newDeptObj).subscribe({
      next:(result:any)=>{
      this.getAllDepartments();
      }, error:(error:any)=>{

      }
    })
  }

   onUpdateDept(){
   this.masterService.updateDept(this.newDeptObj).subscribe({
      next:(result:any)=>{
      this.getAllDepartments();
      }, error:(error:any)=>{

      }
    })
  }

  getAllDepartments(){
    this.masterService.getAllDept().subscribe({
      next:(result:any)=>{
       this.deptList.set(result);
      }
    })
  }

  onEdit(data: DepartmentModel){
     const strData = JSON.stringify(data);
     const parseData = JSON.parse(strData);
     this.newDeptObj = parseData;
  }

  onReset(){
    this.newDeptObj = new DepartmentModel();
  }

  onDelete(id: number){
    const isDelete = confirm("Are you sure you want to delete");
    if(isDelete){
      this.masterService.deleteDeptById(id).subscribe({
      next:(result:any)=>{
      this.getAllDepartments();
      }, error:(error:any)=>{

      }
    })
    }
    
  }
}
