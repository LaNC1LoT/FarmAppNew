import React from "react"
import { Typography } from "@material-ui/core"
import styles from './Greeting.module.css'
const logo = require('../../logo.png')
const graf = require('../../FarmApp.svg')
const home = require('../../home.svg')
const doctor = require('../../doctor.svg')


export const Greeting = () => {


  return(
        <Typography>
          <div className={styles.main}>
            <div style={{display:"flex",justifyContent: "space-between",alignItems: "center"}}>
              <img style={{width: '25%'}} src={logo} alt=""/>
              <div style={{width: '40%',textAlign: 'justify'}}>
                FarmApp может быть использована как непосредственно на местах (в маркетинговых отделах фармацевтической компании или сети аптек) для мониторинга и анализа фармацевтического рынка путем внесения полученных данных, так и в управляющем органе (министерство или ведомство).
              </div>
            </div>
            <div>
              <div style={{width: '60%',height: '4px',backgroundColor: 'black' ,borderRadius: '15px 15px 15px 15px',margin: '65px auto 0'}}></div>
              <div style={{width: '80%',height: '4px',backgroundColor: 'rgb(127, 8, 201)',borderRadius: '15px 15px 15px 15px',margin: '5px auto'}}></div>
              <div style={{width: '60%',height: '4px',backgroundColor: 'black' ,borderRadius: '15px 15px 15px 15px',margin: '0 auto 65px'}}></div>
            </div>
            <div style={{display:"flex",justifyContent: "space-between",alignItems: "center"}}>
              <div style={{width: '40%',textAlign: 'justify'}}>
                Благодаря FarmApp повысится конкурентоспособность учреждения, где будет использоваться данное программное обеспечение, а также будут реализовываться только те медикаменты, которые пользуются спросом.
                Программный комплекс позволяет вести мониторинг и анализ фармацевтического рынка, ликвидировать несбалансированность поставляемых препаратов потребительскому спросу, а также использовать полученные данные в других информационных системах.

              </div>
              <img src={graf} style={{width: '50%',marginTop: '20px'}} alt="graf"/>
            </div>
            <div>
              <div style={{width: '60%',height: '4px',backgroundColor: 'black' ,borderRadius: '15px 15px 15px 15px',margin: '65px auto 0'}}></div>
              <div style={{width: '80%',height: '4px',backgroundColor: 'rgb(127, 8, 201)',borderRadius: '15px 15px 15px 15px',margin: '5px auto'}}></div>
              <div style={{width: '60%',height: '4px',backgroundColor: 'black' ,borderRadius: '15px 15px 15px 15px',margin: '0 auto 65px'}}></div>
            </div>
            <div style={{display:"flex",justifyContent: "space-between",alignItems: "center"}}>
              <img style={{width: '50%'}} src={home} alt="home"/>
              <div style={{width: '40%',textAlign: 'justify'}}>
                В качестве основных возможностей FarmApp в отличие от наших конкурентов, стоит отметить автоматизацию маркетинговых исследований, разделенный доступ к информации, учет фармацевтических препаратов, построение диаграмм исследований и воронок продаж, поиск необходимой информации, формирование списка самых популярных лекарств, изучение спроса на фармацевтические препараты, развитие рынка фармацевтики, оптимизацию и повышение эффективности планирования занятости сотрудников, поставку жизненно необходимых препаратов, отсутствующих в поставках в принципе, но обладающих спросом.
                Четкая методология и автоматизация системы учёта медикаментов, а также мониторинга и анализа рынка, являются неотъемлемой частью успешного функционирования любой аптеки или аптечного склада.
              </div>
            </div>
            <div>
              <div style={{width: '60%',height: '4px',backgroundColor: 'black' ,borderRadius: '15px 15px 15px 15px',margin: '65px auto 0'}}></div>
              <div style={{width: '80%',height: '4px',backgroundColor: 'rgb(127, 8, 201)',borderRadius: '15px 15px 15px 15px',margin: '5px auto'}}></div>
              <div style={{width: '60%',height: '4px',backgroundColor: 'black' ,borderRadius: '15px 15px 15px 15px',margin: '0 auto 65px  '}}></div>
            </div>
            <div style={{display:"flex",justifyContent: "space-between",alignItems: "center"}}>
              <div style={{width: '40%',textAlign: 'justify'}}>
                Мониторинг и анализ фармацевтического рынка обеспечивает рентабельность производства фармацевтических препаратов, прогнозирует изменение рыночной ситуации в сфере фармацевтического обслуживания, анализирует тенденции изменения структуры платежеспособного спроса на медикаменты и фармацевтические услуги, определяет пути достижения требуемого качества. Для маркетинга в здравоохранении вообще и фармацевтического маркетинга в частности наиболее важно следующее: прежде чем производить новый лекарственный препарат или предлагать новый вид медицинской услуги, необходимо убедиться, что в них есть нужда у конкретного потребителя.
              </div>
              <img src={doctor} style={{width: '50%',marginTop: '20px'}} alt="Doctor"/>
            </div>
          </div>


        </Typography>
    )
}
/*









*/
