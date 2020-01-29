import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarNavComponent } from './sidebar-nav.component';
import { SidebarModule } from '../sidebar.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('SidebarNavComponent', () => {
  let component: SidebarNavComponent;
  let fixture: ComponentFixture<SidebarNavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ SidebarModule, RouterTestingModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

it('should create', () => {
    expect(component).toBeTruthy();
  });

});
