import { Component } from '@angular/core';
import { CatalogService } from '../services/catalog.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {

    constructor(private catalogService:CatalogService)
    {
       catalogService.getRoles().subscribe(r=>{
        console.log(r);
       })
    }
}
