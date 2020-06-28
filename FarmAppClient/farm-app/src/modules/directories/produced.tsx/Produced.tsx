import React, { useEffect } from 'react';
import { Typography } from '@material-ui/core';
import TreeList, {
  Editing,
  SearchPanel,
  Column,
  RequiredRule,
  Selection,
  Sorting,
  Scrolling,
  Paging,
  Pager, HeaderFilter, FilterRow, Lookup,
} from 'devextreme-react/tree-list';
import { vendors } from '../../../api/mock/vendors';
import { BASE_URL } from '../../../core/constants';
import AspNetData from 'devextreme-aspnet-data-nojquery';
import { connect } from 'react-redux';
import { IAppState } from '../../../core/mainReducer';

const Produced = ({ user }: { user: any }) => {
  const allowedPageSizes = [20, 50, 100];
  const onCellPrepared = (e: any) => {
    if (e.column.command === 'edit') {
      let addLink = e.cellElement.querySelector('.dx-link-add');

      if (addLink) {
        addLink.remove();
      }
    }
  };
  useEffect(() => {
    document.title = 'Производители'
  })

  const url = `${BASE_URL}api/Vendors`;
  const userData: any = localStorage.getItem('auth')
  const token: any = userData ? JSON.parse(userData).token : user?.token ?? ""
  const vendorData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${url}?page=1&pageSize=2000`,
    insertUrl: `${url}`,
    updateUrl: `${url}`,
    deleteUrl: `${url}`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: 'Bearer ' + token,
      };
    },
  });


  const regionData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Regions?page=1&pageSize=2000`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: 'Bearer ' + token,
      };
    },
  });

  return (
    <Typography>
      <TreeList
        id="vendors"
        //@ts-ignore
        dataSource={vendorData}
        showRowLines={true}
        showBorders={true}
        columnAutoWidth={true}
        style={{ height: '85vh' }}
        keyExpr="id"
        onCellPrepared={onCellPrepared}
        columnHidingEnabled={true}
      >
        <Scrolling mode="standard" />
        <Paging
          enabled={true}
          defaultPageSize={20}
        />
        <Pager
          showPageSizeSelector={true}
          allowedPageSizes={allowedPageSizes}
          showInfo={true} />
        <FilterRow visible={true} />
        <Sorting mode="multiple" />
        <Selection mode="single" />
        <SearchPanel visible={true} />
        <HeaderFilter visible={true} />

        {user?.role?.id === 1 && <Editing
          allowAdding={true}
          allowUpdating={true}
          allowDeleting={true}
          mode="row"
        />

        }
        <Column
          caption={'Номер'}
          visible={false}
          dataField={'id'}
          alignment={'left'}
        >
        </Column>
        <Column
          caption={'Имя производителя'}
          dataField={'vendorName'}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>
        {/*<Column*/}
        {/*  caption={'Страна производителя'}*/}
        {/*  dataField={'regionName'}>*/}
        {/*  <RequiredRule/>*/}
        {/*</Column>*/}

        <Column
          caption={"Страна производителя"}
          dataField={"regionId"}
          alignment={'left'}
        >
          <RequiredRule />
          <Lookup dataSource={regionData} valueExpr="id" displayExpr="regionName" />
        </Column>



        <Column
          alignment={'left'}
          caption={'Отечесвтенный'}
          dataField={'isDomestic'}

        >
        </Column>
        {/*<Column*/}
        {/*  caption={"Удалена"}*/}
        {/*  dataType="boolean"*/}
        {/*  dataField={"isDeleted"}>*/}
        {/*</Column>*/}
      </TreeList>
    </Typography>
  );
};

export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user,
  };
})(Produced);
