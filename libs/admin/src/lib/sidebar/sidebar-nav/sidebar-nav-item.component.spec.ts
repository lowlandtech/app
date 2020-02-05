import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarNavItemComponent } from './sidebar-nav-item.component';
import { SidebarModule } from '../sidebar.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('SidebarNavItemComponent', () => {
  let component: SidebarNavItemComponent;
  let fixture: ComponentFixture<SidebarNavItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ SidebarModule, RouterTestingModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarNavItemComponent);
    component = fixture.componentInstance;
    component.item = {
      url: ''
    };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
