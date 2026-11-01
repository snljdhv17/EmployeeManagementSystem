export class EmployeeModel {
  employeeId: number;
  name: string;
  contact: string;
  email: string;
  city: string;
  state: string;
  pincode: string;
  altContactNo: string;
  address: string;
  designationId: number;
  //createdDate: Date;
  //modifiedDate: Date;
  role: string;


  
  constructor() {
    this.employeeId = 0;
    this.name = '';
    this.contact = '';
    this.email = '';
    this.city = '';
    this.state = '';
    this.pincode = '';
    this.altContactNo = '';
    this.address = '';
    this.designationId = 0;
    //this.createdDate = new Date();
    //this.modifiedDate = new Date();
    this.role = '';
  }
}

export interface IEmployeeListModel {
  employeeId: number;
  name: string;
  contact: string;
  email: string;
  city: string;
  state: string;
  pincode: string;
  altContactNo: string;
  address: string;
  designationId: number;
  designationName: string;
  departmentId: number;
  departmentName: string;
  role: string;
  createdDate: string;
  modifiedDate: string;
}
