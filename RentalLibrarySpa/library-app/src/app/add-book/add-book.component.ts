import { Component } from '@angular/core';
import { Book } from '../models/book';
import { CatalogService } from '../services/catalog.service';
import { ToastrService } from 'ngx-toastr';
import { Category } from '../models/category';
import { Binding } from '../models/binding';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent {
  book:Book=new Book();
  categories:Array<Category>=new Array<Category>();
  bindings:Array<Binding>=new Array<Binding>();

  constructor(private catalogService:CatalogService,private toastr:ToastrService){
   
    this.getCategories();
    this.getBindings();
  }

  getCategories()
  {
         this.catalogService.getCategories().subscribe(data=>{
            if(data)
            {
              this.categories = data;
            }
         });
  }

  getBindings()
  {
         this.catalogService.getBindings().subscribe(data=>{
            if(data)
            {
              this.bindings = data;
            }
         });
  }

  handleImageInput(event: any) {
    this.book.formFile = event.target.files[0];
  }

  addBook()
  {
    debugger;
    if(this.book.title && this.book.iSBN && this.book.formFile)
    {
      this.book.noOfAvailableCopies = this.book.noOfCopies;

      this.catalogService.addBook(this.book).subscribe((data)=>{
        if(data)
        {
          this.toastr.success('book added successfully',"Success");
          this.book.title = "";
        }
     });
    }
    else{
      this.toastr.error('book could not be added.',"Error");
    }

  }
}
