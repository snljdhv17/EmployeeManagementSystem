import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { EmployeeModel, IEmployeeListModel } from '../models/employee.model';
import { Observable } from 'rxjs';

@Service()
export class EmployeeService {
    apiUrl: string = "https://localhost:7029/api/";
    http = inject(HttpClient)

    saveEmployee(obj: EmployeeModel){
        return this.http.post(this.apiUrl+ "EmployeeMaster", obj)
    }
    
    getAllEmployees(): Observable<IEmployeeListModel[]>{
          return this.http.get<IEmployeeListModel[]>(this.apiUrl+ "EmployeeMaster")
    }

      getEmpById(id: number): Observable<EmployeeModel>{
          return this.http.get<EmployeeModel>(this.apiUrl+ "EmployeeMaster/"+ id )
    }
}
