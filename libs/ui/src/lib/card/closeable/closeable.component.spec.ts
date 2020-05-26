import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CloseableComponent } from './closeable.component';

describe('CloseableComponent', () => {
  let component: CloseableComponent;
  let fixture: ComponentFixture<CloseableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CloseableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CloseableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
