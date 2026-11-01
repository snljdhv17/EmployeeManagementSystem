import { Service, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DepartmentModel, DesignationListModel, DesignationModel } from '../models/department.model';
import { Observable } from 'rxjs';

@Service()
export class Master {

    apiUrl: string = "https://localhost:7029/api/"
    http = inject(HttpClient);

    getAllDept(){
        return this.http.get(this.apiUrl+ "DepartmentMaster/GetAllDepartments")
    }

     saveDept(obj: DepartmentModel){
        return this.http.post(this.apiUrl+ "DepartmentMaster/AddDepartment", obj)
    }
    
      updateDept(obj: DepartmentModel){
        return this.http.put(this.apiUrl+ "DepartmentMaster/UpdateDepartment", obj)
    }

       deleteDeptById(id: number){
        return this.http.delete(this.apiUrl+ "DepartmentMaster/DeleteDepartment/"+ id)
    }
 getAllDesignation() {
    return this.http.get(
      this.apiUrl + "DesignationMaster/GetAllDesignations"
    );
  }

  getAllDesignations(): Observable<DesignationListModel[]>{
   return this.http.get<DesignationListModel[]>(
      this.apiUrl + "DesignationMaster/GetAllDesignations"
    );
  }

  saveDesignation(obj: DesignationModel) {
    return this.http.post(
      this.apiUrl + "DesignationMaster/AddDesignation",
      obj
    );
  }

updateDesignation(obj: DesignationModel) {
  return this.http.put(
    `${this.apiUrl}DesignationMaster/UpdateDesignation/${obj.designationId}`,
    obj
  );
}

  deleteDesignationById(id: number) {
    return this.http.delete(
      this.apiUrl + "DesignationMaster/DeleteDesignation/" + id
    );
  }

}
