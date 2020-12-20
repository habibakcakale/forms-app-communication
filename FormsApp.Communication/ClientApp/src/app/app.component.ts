import {Component, OnDestroy, OnInit} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {IStreamSubscriber} from "@microsoft/signalr/src/Stream";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy, OnInit {
  private uInt8GeneratorConnection?: HubConnection;
  private charGeneratorConnection?: HubConnection;
  newChar?: number;
  newUInt8?: string;

  async ngOnInit() {
    this.charGeneratorConnection = await this.connectToHub("/char-generator");
    this.charGeneratorConnection?.stream<number>("generate").subscribe(this.createSubscriber(val => this.newChar = val));

    this.uInt8GeneratorConnection = await this.connectToHub("/uint8-generator");
    this.uInt8GeneratorConnection?.stream<string>("generate").subscribe(this.createSubscriber(val => this.newUInt8 = val));
  }

  async connectToHub(hubUrl: string) {
    const hubBuilder = new HubConnectionBuilder();
    const connection = hubBuilder.withUrl(hubUrl).build();
    await connection.start();
    return connection;
  }

  ngOnDestroy() {
    return Promise.all([this.uInt8GeneratorConnection?.stop(), this.charGeneratorConnection?.stop()])
  }

  createSubscriber<T>(next: (val: T) => void): IStreamSubscriber<T> {
    return {
      next: next,
      error(err: any) {
        console.log(err);
      },
      complete() {

      }
    }
  }
}
