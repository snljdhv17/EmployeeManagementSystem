export class DepartmentModel{
   departmentId : number;
   departmentName: string;
   isActive: boolean;

   constructor(){
    this.departmentId= 0;
    this.departmentName = "";
    this.isActive = false;
   }
    
}

export class DesignationModel {
  designationId: number;
  departmentId: number;
  designationName: string;


  constructor() {
    this.designationId = 0;
    this.departmentId = 0;
    this.designationName = '';
  }
}

  export class DesignationListModel {
  designationId: number;
  departmentId: number;
  designationName: string;
  departmentName: string | undefined;
 

  constructor() {
    this.designationId = 0;
    this.departmentId = 0;
    this.designationName = '';
    this.departmentName = "";
  }
}