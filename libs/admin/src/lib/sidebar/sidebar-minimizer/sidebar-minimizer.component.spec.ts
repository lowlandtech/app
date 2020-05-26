import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarMinimizerComponent } from './sidebar-minimizer.component';

describe('SidebarMinimizerComponent', () => {
  let component: SidebarMinimizerComponent;
  let fixture: ComponentFixture<SidebarMinimizerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SidebarMinimizerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SidebarMinimizerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
