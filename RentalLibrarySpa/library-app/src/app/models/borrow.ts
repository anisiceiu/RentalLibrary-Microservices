export class Borrow{
    id? :number;
    userId? :number;
    memberId? :number;
    bookId? :number;
    requestId? :number;
    dateBorrowed?:Date;
    dueDate? :Date;
    fees?:number;
    bookName:string="";
}