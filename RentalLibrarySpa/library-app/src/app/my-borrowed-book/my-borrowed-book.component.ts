import { Component } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Borrow } from '../models/borrow';
import { ToastrService } from 'ngx-toastr';
import { BorrowService } from '../services/borrow.service';

@Component({
  selector: 'app-my-borrowed-book',
  templateUrl: './my-borrowed-book.component.html',
  styleUrls: ['./my-borrowed-book.component.css']
})
export class MyBorrowedBookComponent {
  pageSize: number = 10;
  bookBorrowedList: Array<Borrow>;
  paginatedBookBorrowedList: Array<Borrow> = new Array<Borrow>();

  constructor(private borrowService: BorrowService, private toastr: ToastrService) {

    this.bookBorrowedList = new Array<Borrow>();
    this.getbookBorrowList();
  }

  getbookBorrowList() {
    this.borrowService.getMyBorrowedBook().subscribe((data) => {
      if (data) {
        this.bookBorrowedList = data;
        this.paginatedBookBorrowedList = this.bookBorrowedList.slice(0,this.pageSize);
      }
    },
      err => {
        console.log('error');
      })
  }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.paginatedBookBorrowedList = this.bookBorrowedList.slice(startItem, endItem);
  }

}
