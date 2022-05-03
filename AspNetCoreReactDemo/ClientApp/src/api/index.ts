import { ModelType } from '../models/modelType'
import { query, reqInit } from './helpers'

export async function postDemoFilter(modelType: ModelType) {
  return await query<ModelType>('/api/demofilter', reqInit('POST', modelType))
}
