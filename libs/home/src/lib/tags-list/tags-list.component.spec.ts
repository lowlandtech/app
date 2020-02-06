import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { TagsListComponent } from './tags-list.component';

describe('TagsListComponent', () => {
  let component: TagsListComponent;
  let fixture: ComponentFixture<TagsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TagsListComponent ],
      imports: [RouterTestingModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TagsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
