import { Component } from '@angular/core';
import { CatalogService } from '../services/catalog.service';
import { Book } from '../models/book';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { BorrowService } from '../services/borrow.service';
import { ReserveRequest } from '../models/request';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {

  pageSize:number=10;
  bookList: Array<Book>;
  paginatedBookList:Array<Book>= new Array<Book>();
  constructor(private catalogService: CatalogService, private borrowService: BorrowService,private toastr:ToastrService) {

    this.bookList = new Array<Book>();
    this.getbookList();
  }



  reserveRequest(book: Book) {
    var request = new ReserveRequest();
    request.bookId = book.id || 0;
    request.fromDate = new Date();
    request.toDate = this.borrowService.addDays(new Date(), 3);
    request.requestType = "Reserve";
    request.bookName = book.title;

    this.borrowService.reseveBook(request).subscribe((data) => {
        if(data)
        {
          this.toastr.success("Book reserved Successfully!");
        }
    },error=>{
      this.toastr.error("Could not reserve the book.")
    });
  }

  getbookList() {
    this.catalogService.getBooks().subscribe(data => {
      this.bookList = data;
      this.paginatedBookList = this.bookList.slice(0,this.pageSize)
      console.log(data);
    });
  }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.paginatedBookList = this.bookList.slice(startItem, endItem);
  }
  
}
