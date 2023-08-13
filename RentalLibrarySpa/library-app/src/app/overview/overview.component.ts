import { Component } from '@angular/core';
import { CatalogService } from '../services/catalog.service';
import { Book } from '../models/book';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { BorrowService } from '../services/borrow.service';
import { ReserveRequest } from '../models/request';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {

  bookList: Array<Book>;
    constructor(private catalogService:CatalogService,private borrowService:BorrowService)
    {
      
      this.bookList = new Array<Book>();
      this.getbookList();
    }

    

    reserveRequest(book:Book)
    {
      var request = new ReserveRequest(); 
      request.bookId = book.id || 0;
      request.fromDate = new Date();
      request.toDate = this.borrowService.addDays(new Date(),3);
      request.requestType ="Reserve";

      this.borrowService.reseveBook(request).subscribe((data)=>{
        console.log("borrow message:",data);
      });
    }

    getbookList()
    {
      this.catalogService.getBooks().subscribe(data=>{
        this.bookList = data;
        console.log(data);
       });
    }
}
