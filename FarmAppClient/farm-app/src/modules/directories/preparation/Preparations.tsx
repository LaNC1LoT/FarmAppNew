import React, { useEffect } from "react"
import { Typography } from "@material-ui/core"
import TreeList, {
  Editing,
  SearchPanel,
  Column,
  RequiredRule,
  Selection,
  Sorting,
  Scrolling,
  Paging,
  Pager,
  FilterRow,
  RemoteOperations, HeaderFilter, Lookup
} from "devextreme-react/tree-list"
import { preparations } from "../../../api/mock/preparations"
import { BASE_URL } from "../../../core/constants";
import AspNetData from "devextreme-aspnet-data-nojquery";
import { connect } from "react-redux";
import { IAppState } from "../../../core/mainReducer";

export const Preparations = ({ user }: { user: any }) => {
  const allowedPageSizes = [20, 50, 100];
  const onCellPrepared = (e: any) => {
    if (e.column.command === 'edit') {
      let addLink = e.cellElement.querySelector('.dx-link-add');

      if (addLink) {
        addLink.remove();
      }
    }
  }

  useEffect(() => {
    document.title = 'Препараты'
  })


  const url = `${BASE_URL}api/Drugs`;
  const drugsData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${url}?page=1&pageSize=2000`,
    insertUrl: `${url}`,
    updateUrl: `${url}`,
    deleteUrl: `${url}`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: `Bearer ${user.token}`
      };
    }
  });




  const vendorData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/Vendors?page=1&pageSize=2000`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: `Bearer ${user.token}`
      };
    }
  });

  const dosageData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/DosageForms?page=1&pageSize=2000`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: `Bearer ${user.token}`
      };
    }
  });


  const AthData = AspNetData.createStore({
    key: 'id',
    loadUrl: `${BASE_URL}api/CodeAthTypes?page=1&pageSize=2000`,
    onBeforeSend: function (method, ajaxOptions) {
      // ajaxOptions.xhrFields = { withCredentials: false };
      ajaxOptions.headers = {
        Authorization: `Bearer ${user.token}`
      };
    }
  });

  return (
    <Typography>
      <TreeList
        id="preparations"
        //@ts-ignore
        dataSource={drugsData}
        showRowLines={true}
        showBorders={true}
        columnAutoWidth={true}
        style={{ height: '85vh' }}
        columnHidingEnabled={true}
        onCellPrepared={onCellPrepared}
      // keyExpr="id"
      // parentIdExpr="codeAthTypeId"
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
          allowUpdating={true}
          allowDeleting={true}
          allowAdding={true}
          mode="row"
        />
        }

        {/*<Column*/}
        {/*  caption={"Номер"}*/}
        {/*  visible={false}*/}
        {/*  dataField={"id"}>*/}
        {/*</Column>*/}

        <Column
          caption={"Название препарата"}
          dataField={"drugName"}
          alignment={'left'}
        >
          <RequiredRule />
        </Column>

        <Column
          caption={"Код группы Атх"}
          dataField={"codeAthTypeId"}
          alignment={'left'}
        >
          <Lookup dataSource={AthData} valueExpr="id" displayExpr="code" />
        </Column>

        <Column
          caption={"Имя производителя"}
          dataField={"vendorId"}
          alignment={'left'}
        >
          <RequiredRule />
          <Lookup dataSource={vendorData} valueExpr="id" displayExpr="vendorName" />
        </Column>

        <Column
          caption={"Форма выпуска"}
          dataField={"dosageFormTypeId"}
          alignment={'left'}
        >
          <RequiredRule />
          <Lookup dataSource={dosageData} valueExpr="id" displayExpr="dosageForm" />
        </Column>


        <Column
          alignment={'left'}
          caption={"Отечественный"}
          dataField={"isDomestic"}
          allowEditing={false}
        >

        </Column>
        <Column
          alignment={'left'}
          caption={"Generic"}
          dataField={"isGeneric"}>
        </Column>


        {/*<Column*/}
        {/*    caption={"Отечесвтенный"}*/}
        {/*    dataField={"isDomestic"}>*/}
        {/*    <RequiredRule />*/}
        {/*</Column>*/}
        {/*<Column*/}
        {/*    caption={"Удалена"}*/}
        {/*    dataType="boolean"*/}
        {/*    dataField={"isDeleted"}>*/}
        {/*</Column>*/}
      </TreeList>
    </Typography>
  )
}

export default connect((state: IAppState) => {
  const { auth } = state;
  return {
    user: auth.user
  }
})(Preparations)
