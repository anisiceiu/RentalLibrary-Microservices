import { Component } from '@angular/core';
import { Category } from '../models/category';
import { Binding } from '../models/binding';
import { CatalogService } from '../services/catalog.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-binding',
  templateUrl: './add-binding.component.html',
  styleUrls: ['./add-binding.component.css']
})
export class AddBindingComponent {
binding:Binding=new Binding();

  constructor(private catalogService:CatalogService,private toastr:ToastrService){
    
  }

  addBinding()
  {
    if(this.binding.name)
    {
      this.catalogService.addBinding(this.binding).subscribe((data)=>{
        if(data)
        {
          this.toastr.success('Binding added successfully',"Success");
          this.binding.name = "";
        }
     });
    }
    else{
      this.toastr.error('Binding could not be added.',"Error");
    }

  }
}
