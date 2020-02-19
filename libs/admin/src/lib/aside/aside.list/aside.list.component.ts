import { Component, OnInit } from '@angular/core';
import { AdminStateFacade } from '../../+state/facades/admin.facade';
import { Observable } from 'rxjs';
import { AsideListItemComponent } from '../aside.list.item';
import { AsideListItemModel } from '../aside.list.item.model';


@Component({
  selector: 'scx-aside-list',
  templateUrl: './aside.list.component.html',
  styleUrls: ['./aside.list.component.scss']
})
export class AsideListComponent implements OnInit {
  public groups$: Observable<AsideListItemModel[]>;

  constructor(private facade: AdminStateFacade, ) { }

  ngOnInit(): void {
    this.groups$ = this.facade.asideItems$;
  }

  addGroupItem(): void {
    this.facade.asideListItemAdd({
      title: "new group",
      component: AsideListItemComponent
    })
  }
}
