import { Component } from '@angular/core';
import { ReserveRequest } from '../models/request';
import { BorrowService } from '../services/borrow.service';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css']
})
export class RequestListComponent {

  pageSize:number=10;
  bookRequestList: Array<ReserveRequest>;
  paginatedBookRequestList:Array<ReserveRequest>= new Array<ReserveRequest>();
  constructor(private borrowService: BorrowService) {

    this.bookRequestList = new Array<ReserveRequest>();
    this.getbookRequestList();
  } 

  getbookRequestList() {
    this.borrowService.getBookRequests().subscribe(data => {
      this.bookRequestList = data;
      this.paginatedBookRequestList = this.bookRequestList.slice(0,this.pageSize)
      console.log(data);
    });
  }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.paginatedBookRequestList = this.bookRequestList.slice(startItem, endItem);
  }
}
