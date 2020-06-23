import React from 'react';

// import Chart, {
//   ArgumentAxis,
//   CommonSeriesSettings,
//   Legend,
//   Series,
//   Tooltip,
//   ValueAxis,
//   ConstantLine,
//   Label
// } from 'devextreme-react/chart';


import PivotGridDataSource from 'devextreme/ui/pivot_grid/data_source';
import Chart, {
  AdaptiveLayout,
  CommonSeriesSettings,
  Size,
  Tooltip,
} from 'devextreme-react/chart';
import PivotGrid, {
  Export,
  FieldChooser,
  Scrolling,
  FieldPanel,
} from 'devextreme-react/pivot-grid';
// import { sales } from './data.js';
import { sales } from './newData';
//@ts-ignore
import Globalize from 'globalize';
import { loadMessages } from 'devextreme/localization';
import CheckBox from 'devextreme-react/check-box';
import styles from './Chart.module.css';
import 'devextreme/localization/globalize/number';
import 'devextreme/localization/globalize/date';
import 'devextreme/localization/globalize/currency';
import 'devextreme/localization/globalize/message';

import deMessages from 'devextreme/localization/messages/de.json';
import ruMessages from 'devextreme/localization/messages/ru.json';

import deCldrData from 'devextreme-cldr-data/de.json';
import ruCldrData from 'devextreme-cldr-data/ru.json';
import supplementalCldrData from 'devextreme-cldr-data/supplemental.json';
import { BASE_URL } from '../../../core/constants';
import { createStore } from 'devextreme-aspnet-data-nojquery';


const summaryDisplayModes = [
  { text: 'None', value: 'none' },
  { text: 'Absolute Variation', value: 'absoluteVariation' },
  { text: 'Percent Variation', value: 'percentVariation' },
  { text: 'Percent of Column Total', value: 'percentOfColumnTotal' },
  { text: 'Percent of Row Total', value: 'percentOfRowTotal' },
  { text: 'Percent of Column Grand Total', value: 'percentOfColumnGrandTotal' },
  { text: 'Percent of Row Grand Total', value: 'percentOfRowGrandTotal' },
  { text: 'Percent of Grand Total', value: 'percentOfGrandTotal' },
];


class ChartComp extends React.Component<{}, {
  store: any,
  locale: any,
  showColumnFields: any,
  showDataFields: any,
  showFilterFields: any,
  showRowFields: any,
  dataSource: any
}> {
  private _chart: any;
  private _pivotGrid: any;
  // onShowColumnFieldsChanged: any;
  // onShowDataFieldsChanged: any;
  // onShowFilterFieldsChanged: any;
  // onShowRowFieldsChanged: any;
  // onContextMenuPreparing: any;

  constructor(props: any) {
    super(props);
    this.state = {
      locale: this.getLocale(),
      store: null,
      showColumnFields: true,
      showDataFields: true,
      showFilterFields: true,
      showRowFields: true,
      dataSource: null,
    };

    this.initGlobalize();
    this._chart;

    this.onShowColumnFieldsChanged = this.onShowColumnFieldsChanged.bind(this);
    this.onShowDataFieldsChanged = this.onShowDataFieldsChanged.bind(this);
    this.onShowFilterFieldsChanged = this.onShowFilterFieldsChanged.bind(this);
    this.onShowRowFieldsChanged = this.onShowRowFieldsChanged.bind(this);
    this.onContextMenuPreparing = this.onContextMenuPreparing.bind(this);


    // this.locales = service.getLocales();
    // this.payments = service.getPayments();
    // this.changeLocale = this.changeLocale.bind(this);
  }

  setSummaryType(args: any, sourceField: any) {
    this.state.dataSource.field(sourceField.index, {
      summaryType: args.itemData.value,
    });

    this.state.dataSource.load();
  }

  onShowColumnFieldsChanged(e: any) {
    this.setState({ showColumnFields: e.value });
  }

  onShowDataFieldsChanged(e: any) {
    this.setState({ showDataFields: e.value });
  }

  onShowFilterFieldsChanged(e: any) {
    this.setState({ showFilterFields: e.value });
  }

  onShowRowFieldsChanged(e: any) {
    this.setState({ showRowFields: e.value });
  }

  onContextMenuPreparing(e: any) {
    var sourceField = e.field;

    if (e.field && e.field.dataField === 'amount') {
      summaryDisplayModes.forEach(mode => {
        e.items.push({
          text: mode.text,
          selected: e.field.summaryDisplayMode === mode.value,
          onItemClick: () => {
            var format,
              caption = mode.value === 'none' ? 'Total Sales' : 'Relative Sales';
            if (mode.value === 'none'
              || mode.value === 'absoluteVariation') {
              format = 'currency';
            }
            this.state.dataSource.field(e.field.index, {
              summaryDisplayMode: mode.value,
              format: format,
              caption: caption,
            });

            this.state.dataSource.load();
          },
        });
      });
    }

    if (sourceField) {
      if (!sourceField.groupName || sourceField.groupIndex === 0) {
        e.items.push({
          text: 'Hide field',
          onItemClick: function() {
            var fieldIndex;
            if (sourceField.groupName) {
              fieldIndex = this.state.dataSource.getAreaFields(sourceField.area, true)[sourceField.areaIndex].index;
            } else {
              fieldIndex = sourceField.index;
            }

            this.state.dataSource.field(fieldIndex, {
              area: null,
            });
            this.state.dataSource.load();
          },
        });
      }

      if (sourceField.dataType === 'number') {
        var menuItems: any = [];

        e.items.push({ text: 'Summary Type', items: menuItems });
        ['Sum', 'Avg', 'Min', 'Max'].forEach(summaryType => {
          var summaryTypeValue = summaryType.toLowerCase();

          menuItems.push({
            text: summaryType,
            value: summaryType.toLowerCase(),
            onItemClick: function(args: any) {
              this.setSummaryType(args, sourceField);
            },
            selected: e.field.summaryType === summaryTypeValue,
          });
        });
      }
    }
  }

  getLocale() {
    const locale = sessionStorage.getItem('locale');
    return locale != null ? locale : 'ru';
  }

  setLocale(locale: any) {
    sessionStorage.setItem('locale', locale);
  }

  initGlobalize() {
    Globalize.load(
      deCldrData,
      ruCldrData,
      supplementalCldrData,
    );
    // Globalize.loadMessages(deMessages);
    Globalize.loadMessages(ruMessages);
    // Globalize.loadMessages(service.getDictionary());
    Globalize.locale('ru');
  }

  async componentDidMount() {
    //@ts-ignore
    this._pivotGrid.bindChart(this._chart, {
      dataFieldsDisplayMode: 'splitPanes',
      alternateDataFields: false,
    });

    let response: any = await fetch(`${BASE_URL}api/Sales?page=1&pageSize=1000`);
    if (response.ok) {
      let json = await response.json();
      this.setState({
        store: json,
        dataSource: new PivotGridDataSource({
          fields: [
            {
              caption: 'Название препарата',
              dataField: 'drugName',

            },
            {
              caption: 'Название аптеки',
              dataField: 'pharmacyName',
              //@ts-ignore
              area: 'row',

            },
            {
              caption: 'Дата продажи',
              dataField: 'saleDate',
              //@ts-ignore
              dataType: 'date',
              //@ts-ignore
              area: 'column',
            },
            {
              caption: 'Цена за ед.',
              dataField: 'price',
              summaryType: 'sum',
              format: '#,##0.00',
              selector: function(data: any) {
                return data.price;
              },

            },
            {
              caption: 'Кол-во',
              dataField: 'quantity',
              summaryType: 'sum',
              format: '#,##0.00',
              selector: function(data: any) {
                return data.quantity;
              },

            },
            {
              caption: 'Сумма',
              dataField: 'amount',
              summaryType: 'sum',
              format: '#,##0.00',
              selector: function(data: any) {
                return data.amount;
              },

              //@ts-ignore
              area: 'data',

            },
            {
              caption: 'Дисконт',
              dataField: 'isDiscount',
              //@ts-ignore
              dataType: 'boolean',

            },
            {
              dataField: 'Id',
              visible: false,
            },
            {
              dataField: 'drugId',
              visible: false,
            },
            {
              dataField: 'id',
              visible: false,
            },
            {
              dataField: 'isDeleted',
              visible: false,
            },
            {
              dataField: 'pharmacyId',
              visible: false,
            },
            {
              dataField: 'saleImportFileName',
              visible: false,
            },
          ],
          store: json?.data,
        }),
      });
    }
    // setTimeout(function() {
    //   dataSource.expandHeaderItem('row', ['North America']);
    //   dataSource.expandHeaderItem('column', [2013]);
    // });
  }

  render() {
    return (
      <React.Fragment>
        <Chart ref={(ref) => {
          //@ts-ignore
          if (ref?.instance) {
            this._chart = ref.instance;
          }
        }}>
          <Size height={200}/>

          <CommonSeriesSettings type="bar"/>
          <AdaptiveLayout width={450}/>
        </Chart>

        <PivotGrid
          id="pivotgrid"
          //@ts-ignore
          dataSource={
            this.state.dataSource}
          allowSorting={true}
          allowSortingBySummary={true}
          allowFiltering={true}
          height={620}
          showBorders={true}
          rowHeaderLayout="tree"

          showColumnTotals={false}
          showColumnGrandTotals={true}
          showRowTotals={false}
          showRowGrandTotals={true}


          //@ts-ignore
          ref={(ref) => {
            if (ref?.instance) {
              this._pivotGrid = ref.instance;
            }
          }}
        >

          <FieldPanel
            showColumnFields={this.state.showColumnFields}
            showDataFields={this.state.showDataFields}
            showFilterFields={this.state.showFilterFields}
            showRowFields={this.state.showRowFields}
            allowFieldDragging={true}
            visible={true}

          />
          <Export enabled={true} fileName="Sales"/>
          <FieldChooser enabled={true}/>
          <Scrolling mode="virtual"/>
        </PivotGrid>
        <div className={styles.options}>
          <div className={styles.caption}>Опции</div>
          <div className={styles.option}>
            <CheckBox id="show-data-fields"
                      value={this.state.showColumnFields}
                      onValueChanged={this.onShowColumnFieldsChanged}
                      text="Показать поле колонок" />
          </div>
          <div className={styles.option}>
            <CheckBox id="show-row-fields"
                      value={this.state.showDataFields}
                      onValueChanged={this.onShowDataFieldsChanged}
                      text="Показать поле данных" />
          </div>
          <div className={styles.option}>
            <CheckBox id="show-column-fields"
                      value={this.state.showFilterFields}
                      onValueChanged={this.onShowFilterFieldsChanged}
                      text="Показать поле фильтров" />
          </div>
          <div className={styles.option}>
            <CheckBox id="show-filter-fields"
                      value={this.state.showRowFields}
                      onValueChanged={this.onShowRowFieldsChanged}
                      text="Показать поле строк" />
          </div>
        </div>
      </React.Fragment>
    );
  }
}

export default ChartComp;
