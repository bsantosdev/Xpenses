import { Component } from '@angular/core';
import { Subject, combineLatest, map, of } from 'rxjs';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  streamOne$ = of('Value 1');
  streamTwo$ = of('Value 2');
  streamThree$ = of('Value 3');

  // combine latest will emit the latest value from each stream and only when all streams have emitted at least once
  vm$ = combineLatest([
    this.streamOne$, 
    this.streamTwo$, 
    this.streamThree$
  ]).pipe(map(([streamOne, streamTwo, streamThree]) => ({ streamOne, streamTwo, streamThree })));
  
  constructor() { }

  ngOnInit(): void { }
}
