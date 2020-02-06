import { Component, DebugElement } from '@angular/core';
import { ComponentFixture, TestBed, async } from '@angular/core/testing';
import { MockRender } from 'ng-mocks';
import { By } from '@angular/platform-browser';
import { BrandMinimizerDirective } from './brand-minimizer.directive';
import { AdminModule } from '../../admin.module';
import { StoreModule } from '@ngrx/store';

@Component({
  selector: 'scx-test-component',
  template: ''
})
class TestComponent {}

describe('BrandMinimizerDirective', () => {
  let component: TestComponent;
  let directiveEl: DebugElement;
  let fixture: ComponentFixture<TestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdminModule
      ],
      declarations: [
        TestComponent
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = MockRender(
      `<scx-test-component scxBrandMinimizer></scx-test-component>`
    );
    component = fixture.componentInstance;
    directiveEl = fixture.debugElement.query(
      By.directive(BrandMinimizerDirective)
    );
    fixture.detectChanges();
  });

  it('should create a host instance', () => {
    expect(component).toBeTruthy();
  });

  it('should create a directive instance', () => {
    expect(directiveEl).toBeTruthy();
  });

  it('should create an instance', () => {
    const directive = directiveEl.injector.get(BrandMinimizerDirective);
    expect(directive).toBeTruthy();
  });

});
