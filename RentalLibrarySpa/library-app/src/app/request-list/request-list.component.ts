import { Component } from '@angular/core';
import { ReserveRequest } from '../models/request';
import { BorrowService } from '../services/borrow.service';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css']
})
export class RequestListComponent {

  pageSize:number=10;
  bookRequestList: Array<ReserveRequest>;
  paginatedBookRequestList:Array<ReserveRequest>= new Array<ReserveRequest>();
  constructor(private borrowService: BorrowService,private toastr:ToastrService) {

    this.bookRequestList = new Array<ReserveRequest>();
    this.getbookRequestList();
  } 

  getbookRequestList() {
    this.borrowService.getBookRequests().subscribe(data => {
      this.bookRequestList = data;
      this.paginatedBookRequestList = this.bookRequestList.slice(0,this.pageSize)
    });
  }

  acceptRequest(request:ReserveRequest)
  {
    this.borrowService.issueBook(request).subscribe((data)=>{
       if(data)
       {
        this.toastr.success("Book Issued Successfully!");
       }
    },error=>{
      this.toastr.error("Book Issued Failed!");
    });
  }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.paginatedBookRequestList = this.bookRequestList.slice(startItem, endItem);
  }
}
