export class ReserveRequest{
    id?:number;
    memberId:number=0;
    bookId:number=0;
    requestType:string="Reserve";
    fromDate:Date=new Date();
    toDate:Date = new Date();
}