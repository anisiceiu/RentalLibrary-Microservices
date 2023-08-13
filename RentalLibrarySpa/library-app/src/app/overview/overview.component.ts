import { Component } from '@angular/core';
import { CatalogService } from '../services/catalog.service';
import { Book } from '../models/book';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {

  bookList: Array<Book>;
    constructor(private catalogService:CatalogService)
    {
      this.bookList = new Array<Book>();
      this.getbookList();
    }

    getbookList()
    {
      this.catalogService.getBooks().subscribe(data=>{
        this.bookList = data;
        console.log(data);
       });
    }
}
