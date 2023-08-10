export class Book {
    id?: number;
    title: string="";
    author:string="";
    iSBN: string="";
    publishYear: number=1000;
    language: string="";
    noOfCopies: number=0;
    noOfAvailableCopies: number=0;
    bindingId: number=0;
    categoryId: number=0;
    categoryName: string="";
    bindingName: string="";
    thumbnailImageUrl?: string;
    formFile:any;
}