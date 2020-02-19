import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AsideListItemComponent } from './aside.list.item.component';

describe('AsideListItemComponent', () => {
  let component: AsideListItemComponent;
  let fixture: ComponentFixture<AsideListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AsideListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AsideListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
