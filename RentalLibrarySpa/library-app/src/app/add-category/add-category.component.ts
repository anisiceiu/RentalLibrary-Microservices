import { Component } from '@angular/core';
import { Category } from '../models/category';
import { CatalogService } from '../services/catalog.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  category:Category=new Category();

  constructor(private catalogService:CatalogService,private toastr:ToastrService){
    
  }

  addCategory()
  {
    if(this.category.name)
    {
      this.catalogService.addCategory(this.category).subscribe((data)=>{
        if(data)
        {
          this.toastr.success('Category added successfully',"Success");
          this.category.name = "";
        }
     });
    }
    else{
      this.toastr.error('Category could not be added.',"Error");
    }

  }
}


